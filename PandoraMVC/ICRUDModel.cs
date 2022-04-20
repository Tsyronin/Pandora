using System.Collections.Generic;

namespace PandoraMVC.Controllers
{
    public class ICRUDModel<T> where T : class
    {

        public object key { get; set; }

        public T value { get; set; }

        public List<T> added { get; set; }

        public List<T> changed { get; set; }

        public List<T> deleted { get; set; }

    }
}