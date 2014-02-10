using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMobileDataModel
{
    public interface IDataAccess<T>
    {
        T read(String name);
        IEnumerable<T> readAll();
        void write(T t);
        bool update(T t);
        bool delete(T t);
    }

    public class EstateDBAccess : IDataAccess<Estate>
    {
        private static List<Estate> DB =
            new List<Estate> 
            {
                new Estate{ 
                    nameShort="Casa 1", 
                    pricePerWeek = 120,
                    location=new Location{ 
                        name="Lisboa", 
                        coordinates=new MapCoordinate{
                            latitude=38.732887,
                            longitude=-9.150538
                        }
                    },
                    averageRate=3,
                    testimonials = new List<Testimonial>{
                        new Testimonial{
                            stars=3,
                            description="Uma casa que Deus me livre",
                            author="Admin",
                            day= new DateTime(2014,1,15)
                        }
                    }, 
                    characteristics = new Characteristics{
                        capacity=3,
                        dimension=500,
                        divisions=4,
                        rooms=4
                    },
                    availability = new Availability{
                        occupied =  new List<DateInterval>{
                            new DateInterval{
                                from = new DateTime(2014,1,20),
                                to = new DateTime(2014,1,26)
                            }
                        }
                    },
                    descriptionLong="Descricao como por exemplo detalhes e cenas", 
                    photos = new Photos{
                        mainPhoto="2078752.jpg",
                        otherPhotos = new List<String>{
                            "1386174118134.jpg",
                            "5274239.jpg"
                        }
                    }
                },
                new Estate{ nameShort="Casa 2", pricePerWeek = 90, location=new Location{ name="Porto", coordinates=new MapCoordinate{latitude=41.1545,longitude=-8.6418}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=4,dimension=500,divisions=4,rooms=4}, descriptionLong="Descricao como por exemplo detalhes e cenas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="1386174118134.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 3", pricePerWeek = 200, location=new Location{ name="Viseu", coordinates=new MapCoordinate{latitude=40.6579,longitude=-7.9089}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=6,dimension=500,divisions=4,rooms=4}, descriptionLong="Ainda outras assoalhadas mas as mesmas cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="1386174118134.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 4", pricePerWeek = 230, location=new Location{ name="Coimbra", coordinates=new MapCoordinate{latitude=40.2036,longitude=-8.4094}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=4,dimension=500,divisions=4,rooms=4}, descriptionLong="Outras assoalhadas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="5274239.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 5", pricePerWeek = 220, location=new Location{ name="Porto", coordinates=new MapCoordinate{latitude=41.1645,longitude=-8.6418}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=2,dimension=500,divisions=4,rooms=4}, descriptionLong="Outras assoalhadas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="4292813.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 6", pricePerWeek = 150, location=new Location{ name="Setubal", coordinates=new MapCoordinate{latitude=38.5216,longitude=-8.8771}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=8,dimension=500,divisions=4,rooms=4}, descriptionLong="Outras assoalhadas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="5274239.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 7", pricePerWeek = 120,location=new Location{ name="Coimbra", coordinates=new MapCoordinate{latitude=40.2036,longitude=-8.4094}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=3,dimension=500,divisions=4,rooms=4}, descriptionLong="Descricao como por exemplo detalhes e cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="2078752.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 8", pricePerWeek = 90, location=new Location{ name="Viseu", coordinates=new MapCoordinate{latitude=40.6579,longitude=-7.9089}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=4,dimension=500,divisions=4,rooms=4}, descriptionLong="Descricao como por exemplo detalhes e cenas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="1386174118134.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 9", pricePerWeek = 200, location=new Location{ name="Beja", coordinates=new MapCoordinate{latitude=38.5616,longitude=-7.8799}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=6,dimension=500,divisions=4,rooms=4}, descriptionLong="Ainda outras assoalhadas mas as mesmas cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="1386174118134.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 10", pricePerWeek = 230, location=new Location{ name="Castelo Branco", coordinates=new MapCoordinate{latitude=39.8170,longitude=-7.4990}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=4,dimension=500,divisions=4,rooms=4}, descriptionLong="Outras assoalhadas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="5274239.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 11", pricePerWeek = 220, location=new Location{ name="Viseu", coordinates=new MapCoordinate{latitude=40.6579,longitude=-7.9089}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=2,dimension=500,divisions=4,rooms=4}, descriptionLong="Outras assoalhadas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="4292813.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 12", pricePerWeek = 150, location=new Location{ name="Lisboa", coordinates=new MapCoordinate{latitude=38.83008,longitude=-9.1700}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=8,dimension=500,divisions=4,rooms=4}, descriptionLong="Outras assoalhadas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="5274239.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 13", pricePerWeek = 120,location=new Location{ name="Coimbra", coordinates=new MapCoordinate{latitude=40.2036,longitude=-8.4094}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=3,dimension=500,divisions=4,rooms=4}, descriptionLong="Descricao como por exemplo detalhes e cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="2078752.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 14", pricePerWeek = 90, location=new Location{ name="Porto", coordinates=new MapCoordinate{latitude=41.1645,longitude=-8.6418}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=4,dimension=500,divisions=4,rooms=4}, descriptionLong="Descricao como por exemplo detalhes e cenas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="1386174118134.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 15", pricePerWeek = 200, location=new Location{ name="Lisboa", coordinates=new MapCoordinate{latitude=38.7315,longitude=-9.1718}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=6,dimension=500,divisions=4,rooms=4}, descriptionLong="Ainda outras assoalhadas mas as mesmas cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="1386174118134.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 16", pricePerWeek = 230, location=new Location{ name="Porto", coordinates=new MapCoordinate{latitude=41.1645,longitude=-8.6418}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=4,dimension=500,divisions=4,rooms=4}, descriptionLong="Outras assoalhadas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="5274239.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 17", pricePerWeek = 220, location=new Location{ name="Coimbra", coordinates=new MapCoordinate{latitude=40.2036,longitude=-8.4194}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=2,dimension=500,divisions=4,rooms=4}, descriptionLong="Outras assoalhadas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="4292813.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 18", pricePerWeek = 150, location=new Location{ name="Lagos", coordinates=new MapCoordinate{latitude=37.1079,longitude=-8.6867}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=8,dimension=500,divisions=4,rooms=4}, descriptionLong="Outras assoalhadas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="5274239.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 19", pricePerWeek = 120,location=new Location{ name="Lisboa", coordinates=new MapCoordinate{latitude=38.730887,longitude=-9.150538}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=3,dimension=500,divisions=4,rooms=4}, descriptionLong="Descricao como por exemplo detalhes e cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="2078752.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}},
                new Estate{ nameShort="Casa 20", pricePerWeek = 90, location=new Location{ name="Faro", coordinates=new MapCoordinate{latitude=37.0366,longitude=-7.9163}}, testimonials = new List<Testimonial>(), characteristics= new Characteristics{capacity=4,dimension=500,divisions=4,rooms=4}, descriptionLong="Descricao como por exemplo detalhes e cenas e outras cenas", availability = new Availability{ occupied =  new List<DateInterval>()}, photos = new Photos{mainPhoto="1386174118134.jpg",otherPhotos = new List<String>{"1386174118134.jpg"}}}
            };
        public Estate read(String name)
        {
            foreach (Estate house in DB)
            {
                if (house.nameShort.Equals(name))
                    return house;
            }
            return null;
        }

        public IEnumerable<Estate> readAll()
        {
            return DB;
        }

        public void write(Estate t)
        {
            DB.Add(t);
        }

        public bool update(Estate t)
        {
            return false;
        }

        public bool delete(Estate t)
        {
            return false;
        }
    }
}
