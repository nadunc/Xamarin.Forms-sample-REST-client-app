using EAD_Posts_App.Data;
using EAD_Posts_App.Net;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EAD_Posts_App
{
    public partial class PostsListPage : ContentPage
    {
        public ObservableCollection<Post> PostsList;
        DataRetriever _dr;


        public PostsListPage()
        {
            InitializeComponent();

            Title = "Posts";
            PostsList = new ObservableCollection<Post>();
            PostsListView.ItemsSource = PostsList;

            PostsListView.ItemSelected += PostsListView_ItemSelected;

            _dr = new DataRetriever();
            LoadData();
        }


        private void LoadData()
        {
            Task.Factory.StartNew(() => {

                PostsList.Clear();
                
                List<Post> posts = _dr.GetPosts();

                foreach (Post p in posts)
                {
                    PostsList.Add(p);
                }

            }).ContinueWith(task => {
                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void PostsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Post post = e.SelectedItem as Post;
                CommentsListPage commentsListPage = new CommentsListPage(post);
                Navigation.PushAsync(commentsListPage);

            }
            
            PostsListView.SelectedItem = null;
        }
    }
}
