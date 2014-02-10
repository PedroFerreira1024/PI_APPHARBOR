using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMobileDataModel
{
    public class Page<T> where T : Estate
    {
        public double medLat, medLong;
        private int number;
        public List<T> pageList = new List<T>();

        public void setPage(List<T> list)
        {
            pageList.AddRange(list);
        }

        public int getPageNumber()
        {
            return number;
        }

        public void setPageNumber(int num)
        {
            number = num;
        }

        public void setCenter()
        {
            medLat = 39.3365;
            medLong = -8.1628;
        }
    }

    public class PageMap
    {
        public int currentPage = 1;
        public int numberOfPages;
        public Dictionary<int, Page<Estate>> pages;

        public PageMap()
        {
            pages = new Dictionary<int, Page<Estate>>();
        }

        public PageMap (Dictionary<int,Page<Estate>> map)
        {
            pages = map;
            numberOfPages = pages.Count;
            currentPage = 1;
        }

        public int getCurrentPage()
        {
            return currentPage;
        }

        public void setCurrentPage(int idx)
        {
            if (idx <= pages.Count)
                currentPage = idx;

        }

        public void addHouseOnPage(Estate house, int page)
        {
            pages[page].pageList.Add(house);
            pages[page].setPageNumber(page);
            numberOfPages=pages.Count;
        }
    }
}
