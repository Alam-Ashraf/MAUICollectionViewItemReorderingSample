using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MAUICollectionViewItemReorderingSample
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<BannerImage> imageList;

        private BannerImage _itemBeingDragged;

        public MainPageViewModel()
        {
            GetImageList();
        }

        [RelayCommand]
        public void ItemDragged(BannerImage user)
        {
            Debug.WriteLine($"ItemDragged : {user?.ImageName}");

            _itemBeingDragged = user;
        }

        [RelayCommand]
        public void ItemDraggedOver(BannerImage user)
        {
            Debug.WriteLine($"ItemDraggedOver : {user?.ImageName}");

            try
            {
                var itemToMove = _itemBeingDragged;
                var itemToInsertBefore = user;

                if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                    return;

                int insertAtIndex = ImageList.IndexOf(itemToInsertBefore);

                if (insertAtIndex >= 0 && insertAtIndex < ImageList.Count)
                {
                    ImageList.Remove(itemToMove);
                    ImageList.Insert(insertAtIndex, itemToMove);
                }

                Debug.WriteLine($"ItemDropped: [{itemToMove?.ImageName}] => [{itemToInsertBefore?.ImageName}], target index = [{insertAtIndex}]");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void GetImageList()
        {
            ImageList = new ObservableCollection<BannerImage>();

            ImageList.Add(new BannerImage()
            {
                HeaderColor = Color.FromArgb("#F7DC6F"),
                ImageName = "Image img6",
                ImageUrl = ImageSource.FromFile("img6.jpg")
            });

            ImageList.Add(new BannerImage()
            {
                HeaderColor = Color.FromArgb("#7DCEA0"),
                ImageName = "Image img7",
                ImageUrl = ImageSource.FromFile("img7.jpg")
            });

            ImageList.Add(new BannerImage()
            {
                HeaderColor = Color.FromArgb("#7FB3D5"),
                ImageName = "Image img8",
                ImageUrl = ImageSource.FromFile("img8.jpeg")
            });

            ImageList.Add(new BannerImage()
            {
                HeaderColor = Color.FromArgb("#00FF00"),
                ImageName = "Pexels 257840",
                ImageUrl = ImageSource.FromFile("gif4.gif")
            });

            ImageList.Add(new BannerImage()
            {
                HeaderColor = Color.FromArgb("#F0048A"),
                ImageName = "Pexels 257840",
                ImageUrl = ImageSource.FromFile("gif5.gif")
            });

            ImageList.Add(new BannerImage()
            {
                HeaderColor = Color.FromArgb("#D7DBDD"),
                ImageName = "Image img5",
                ImageUrl = ImageSource.FromFile("img5.jpeg")
            });

            ImageList.Add(new BannerImage()
            {
                HeaderColor = Color.FromArgb("#C39BD3"),
                ImageName = "Image img9",
                ImageUrl = ImageSource.FromFile("img9.jpeg")
            });
        }
    }

    public partial class BannerImage : ObservableObject
    {
        [ObservableProperty]
        private ImageSource imageUrl;

        [ObservableProperty]
        private string imageName;

        [ObservableProperty]
        private string imageDesc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";

        [ObservableProperty]
        private bool isSquareView;

        [ObservableProperty]
        private Color headerColor;
    }
}
