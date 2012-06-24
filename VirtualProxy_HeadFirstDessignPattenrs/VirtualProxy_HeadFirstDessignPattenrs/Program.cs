using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualProxy_HeadFirstDessignPattenrs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("(Status: Step 1 - Getting data)\n");
            PhotoAlbum photoAlbum = DataProvider.GetData();
            PhotoAlbum photoAlbumProxy = DataProvider.GetProxyData();

            Console.WriteLine("\n(Status: Step 2 - Starting slideshows)\n");

            photoAlbum.Slideshow();
            photoAlbumProxy.Slideshow();

            Console.WriteLine("(Status: Finished)");

            Console.ReadKey();
        }

        public interface IPhoto
        {
            void Show();
        }

        public class Photo : IPhoto
        {
            public string Name { get; private set; }

            public Photo(string name)
            {
                Name = name;
                Load();
            }

            public void Show()
            {
                Console.WriteLine("----> Displaying photo '{0}')", Name);
            }

            private void Load()
            {
                Console.WriteLine("------> Loading photo '{0}')", Name);
            }
        }

        public class PhotoProxy : IPhoto
        {
            IPhoto photo;

            public string Name { get; private set; }

            public PhotoProxy(string name)
            {
                Name = name;
            }

            public void Show()
            {
                Console.WriteLine("----> Displaying photo '{0}')", Name);
                if (photo == null)
                {
                    photo = new Photo(Name);
                }
            }
        }

        public class PhotoAlbum
        {
            List<IPhoto> photos;

            public string Name { get; private set; }

            public PhotoAlbum(string name)
            {
                Name = name;
                photos = new List<IPhoto>();
            }

            public void AddPhoto(IPhoto photo)
            {
                photos.Add(photo);
            }

            public void Slideshow()
            {
                Console.WriteLine("--> Starting '{0}' slideshow)", Name);
                foreach (IPhoto iPhoto in photos)
                {
                    iPhoto.Show();
                }
                Console.WriteLine("--> Slideshow for '{0}' is finished)\n", Name);
            }
        }

        public static class DataProvider
        {
            public static PhotoAlbum GetData()
            {
                PhotoAlbum photoAlbum = new PhotoAlbum("Album 1");

                photoAlbum.AddPhoto(new Photo("Photo 1"));
                photoAlbum.AddPhoto(new Photo("Photo 2"));
                photoAlbum.AddPhoto(new Photo("Photo 3"));

                return photoAlbum;
            }

            public static PhotoAlbum GetProxyData()
            {
                PhotoAlbum photoAlbum = new PhotoAlbum("Album 2 (Proxy)");

                photoAlbum.AddPhoto(new PhotoProxy("Proxy Photo 1"));
                photoAlbum.AddPhoto(new PhotoProxy("Proxy Photo 2"));
                photoAlbum.AddPhoto(new PhotoProxy("Proxy Photo 3"));

                return photoAlbum;
            }
        }

    }
}
