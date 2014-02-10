using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMobileDataModel
{

    public interface ISearchable<T>
    {
        ISearchable<T> byName(String name);
        ISearchable<T> byLocation(String location);
        ISearchable<T> byPrice(long low, long high);
        ISearchable<T> byAvailability(DateTime from, DateTime to);
        ISearchable<T> byCapacity(int capacity);
        
        //
        //This must be called endSearch, its to be called at the end of the filtering
        IEnumerable<T> getSearch();
    }

    public class SearchUtils : ISearchable<Estate>
    {
        private IEnumerable<Estate> listToSearch;

        private SearchUtils(List<Estate> list)
        {
            listToSearch = list;
        }

        private SearchUtils updateList(List<Estate> newList)
        {
            listToSearch = newList;
            return this;
        }

        //
        //This should be called to initiate search processm, followed bye the prefixed with "by" methods for filter
        public static SearchUtils search(List<Estate> toSearch)
        {
            return new SearchUtils(toSearch);
        }

        public IEnumerable<Estate> getSearch()
        {
            return listToSearch;
        }

        public ISearchable<Estate> byName(String name)
        {
            if (!validateString(name)) return this;

            List<Estate> newList = new List<Estate>();

            foreach (Estate t in listToSearch)
                if (t.nameShort.ToLower().Contains(name.ToLower()))
                    newList.Add(t);

            return updateList(newList);
        }

        public ISearchable<Estate> byLocation(String location)
        {
            if (!validateString(location)) return this;

            List<Estate> newList = new List<Estate>();

            foreach (Estate t in listToSearch)
                if (t.location.name.ToLower().Contains(location.ToLower()))
                    newList.Add(t);

            return updateList(newList);
        }

        public ISearchable<Estate> byPrice(long low, long high)
        {
            if (low == 0 && high == 0) return this;
            if (high == 0) high = -1;

            List<Estate> newList = new List<Estate>();

            foreach (Estate t in listToSearch)
                if (hasPriceBetween(t,low,high))
                    newList.Add(t);

            return updateList(newList);
        }

        private bool hasPriceBetween(Estate estate,long low, long high)
        {
            if (high < 0)
            {
                if (estate.pricePerWeek >= low)
                    return true;
            }
            else
                if (estate.pricePerWeek >= low && estate.pricePerWeek <= high)
                    return true;
            return false;
        }
        
        public ISearchable<Estate> byAvailability(DateTime from, DateTime to)
        {
            if (!verifyDates(from, to))
                return this;

            List<Estate> newList = new List<Estate>();

            foreach (Estate t in listToSearch)
                if (hasAvailabilityBetween(t, from, to))
                    newList.Add(t);            

            return updateList(newList);
        }

        private bool hasAvailabilityBetween(Estate t, DateTime from, DateTime to)
        {
            
            Availability ava = t.availability;
              
            foreach(DateInterval di in ava.occupied){
                if (from.CompareTo(di.from)>=0 && from.CompareTo(di.to) <= 0 ) return false;
                if (to.CompareTo(di.from) >= 0 && to.CompareTo(di.to) <= 0) return false;
                if (from.CompareTo(di.from) <= 0 && to.CompareTo(di.to) >= 0 ) return false;
            }
            return true;
        }
        
        public ISearchable<Estate> byCapacity(int capacity)
        {
            List<Estate> newList = new List<Estate>();

            foreach (Estate t in listToSearch)
                if (t.characteristics.capacity >= capacity)
                    newList.Add(t);

            return updateList(newList);
        }

        private bool validateString(String toValidate)
        {
            if (toValidate == null || toValidate.Length == 0 || toValidate.Trim().Length == 0) 
                return false;
            return true;
        }

        private bool verifyDates(DateTime from, DateTime to)
        {
            if (from.Year == 1 && to.Year == 1) return false;
            if (from.Year == 1 || to.Year == 1)
            {
                if (from.Year == 1) from = to;
                else to = from;
            }
            return true;
        }

    }

}
