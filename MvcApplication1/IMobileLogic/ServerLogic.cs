using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMobileDataModel;
using System.IO;
using System.Web;

namespace IMobileLogic
{
    public interface ILogic<T,K,A>
    {
        //T -> PageMap
        //K -> Estate
        //A ->Testimonial

        //
        //Returns whats supposed to be a single house
        K getHouse(String name);
        //
        //Returns the struct that represents the all pages struct
        T getAllHouses(int numberPerPage);
        //
        //Get the last "lastInsertedNumber" number of houses inserted on DB
        IEnumerable<K> getLastInserted(int lastInsertedNumber);
        //
        //Does the same that getAllHouses but with a max of 7 filters
        T searchAndGetHouses(int numberPerPage, String name, String location, String priceLow, String priceHigh, String availFrom, String availTo, String capacity);
        //
        //Sets on T data struct the current page index
        T setCurrentPage(int index);
        //
        //inserts a new house of type K
        void insertHouse(K house, HttpPostedFileBase mainPhoto, List<HttpPostedFileBase> otherPhotoList, String path);
        //
        //gets a testimonial A from a K house
        A getLastTestimonial(K house);
        //
        //adds a A testimonial to a K house
        void addTestimonial(K house, A test);

    }

    public class ServerLogic:ILogic<PageMap,Estate,Testimonial>
    {
        private readonly int YEAR=2, MONTH=0, DAY=1;

        private PageMap housesInUse;

        private static IDataAccess<Estate> dataAccess = new EstateDBAccess();

        public Estate getHouse(String name)
        {
            return dataAccess.read(name);          
        }

        public PageMap getAllHouses(int numberPerPage)
        {
            setInUse((List<Estate>)dataAccess.readAll(), numberPerPage);

            return housesInUse;
        }

        public IEnumerable<Estate> getLastInserted(int lastInsertedNumber)
        {
            List<Estate> list = (List<Estate>)dataAccess.readAll();
            for (int i = list.Count-1,y=0; i >= 0 && y < lastInsertedNumber; --i,++y)
            {
                yield return list.ElementAt(i);
            }
        }

        class SearchObj
        {
            public String name;
            public String location;
            public long low; 
            public long high;
            public DateTime from; 
            public DateTime to;
            public int cap;
        }

        public PageMap searchAndGetHouses(int numberPerPage, String name, String location, String priceLow, String priceHigh, String availFrom, String availTo, String capacity)
        {
            SearchObj sObj = testSearchParameters(name, location, priceLow, priceHigh, availFrom, availTo,capacity);

            setInUse((List<Estate>)SearchUtils.search((List<Estate>)dataAccess.readAll()).
                byName(sObj.name).byLocation(sObj.location).byPrice(sObj.low, sObj.high).byCapacity(sObj.cap).byAvailability(sObj.from, sObj.to).getSearch(), numberPerPage);

            return housesInUse;
        }

        private SearchObj testSearchParameters(String name, String location, String priceLow, String priceHigh, String availFrom, String availTo, String capacity)
        {
            long low = Convert.ToInt64((priceLow.Length == 0) ? null : priceLow);
            long high = Convert.ToInt64((priceHigh.Length == 0) ? null : priceHigh);
            int cap = Convert.ToInt32((capacity.Length == 0) ? null : capacity);

            String[] parse;
            int year, month, day;
            DateTime from = new DateTime(), to = new DateTime();

            if (availFrom.Length > 0)
            {
                parse = availFrom.Split('/');
                year = Convert.ToInt32((parse[YEAR].Length == 0) ? null : parse[YEAR]);
                month = Convert.ToInt32((parse[MONTH].Length == 0) ? null : parse[MONTH])-1;
                day = Convert.ToInt32((parse[DAY].Length == 0) ? null : parse[DAY]);

                from = new DateTime(year, month, day);
            }

            if (availFrom.Length > 0)
            {
                parse = availTo.Split('/');
                year = Convert.ToInt32((parse[YEAR].Length == 0) ? null : parse[YEAR]);
                month = Convert.ToInt32((parse[MONTH].Length == 0) ? null : parse[MONTH])-1;
                day = Convert.ToInt32((parse[DAY].Length == 0) ? null : parse[DAY]);

                to = new DateTime(year, month, day);
            }
            return new SearchObj { 
                name=name,
                location = location,
                low = low,
                high = high,
                from = from,
                to = to,
                cap = cap
            };
        }

        private void setInUse(List<Estate> list,int numberPerPage)
        {
            housesInUse = new PageMap();
            int page = 1, counter = 0;

            housesInUse.pages[page] = new Page<Estate>();
            foreach(Estate est in list)
            {
                ++counter;
                housesInUse.addHouseOnPage(est, page);
                if (counter % numberPerPage == 0)
                {
                    //Evita criar uma nova entrada no dicionario caso já tenha chegado ao fim da lista
                    if (counter != list.Count)
                    {
                        housesInUse.pages[page].setCenter();
                        ++page;
                        housesInUse.pages[page] = new Page<Estate>();
                    }
                }
            }
        }

        public PageMap setCurrentPage(int index)
        {
            housesInUse.setCurrentPage(index);
            return housesInUse;
        }

        public void insertHouse(Estate house,HttpPostedFileBase mainPhoto, List<HttpPostedFileBase> otherPhotoList, String path)
        {
            house.setImages(mainPhoto,otherPhotoList,path);
            dataAccess.write(house);
        }
        
        public Testimonial getLastTestimonial(Estate house)
        {
            return house.testimonials.Last();
        }

        public void addTestimonial(Estate house, Testimonial test)
        {
            house.testimonials.Add(test);
            float newAvg = 0;

            foreach(Testimonial t in house.testimonials)
            {
                newAvg += t.stars;
            }
            house.averageRate = newAvg / house.testimonials.Count;
        }
    }
}
