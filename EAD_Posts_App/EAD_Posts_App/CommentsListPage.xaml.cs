using EAD_Posts_App.Data;
using EAD_Posts_App.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EAD_Posts_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CommentsListPage : ContentPage
	{
        private Post post;
        public ObservableCollection<Comment> CommentsList;
        DataRetriever _dr;


        public CommentsListPage ()
		{
			InitializeComponent ();
            Title = "Comments";

        }

        public CommentsListPage(Post post) : this()
        {
            this.post = post;

            CommentsList = new ObservableCollection<Comment>();
            CommentsListView.ItemsSource = CommentsList;

            CommentsListView.ItemSelected += CommentsListView_ItemSelected;

            _dr = new DataRetriever();
            AddToolbarItem();
            LoadData();

            
            

        }


        private void LoadData()
        {
            Task.Factory.StartNew(() => {

                CommentsList.Clear();

                List<Comment> comments = _dr.GetCommentsByPostId(post.Id);
                

                foreach (Comment c in comments)
                {
                    CommentsList.Add(c);
                }

            }).ContinueWith(task => {
                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void AddToolbarItem()
        {

            ToolbarItem toolbarItem = new ToolbarItem("User", "ic_user.png", () =>
            {

                UserDetailsPage userDetailsPage = new UserDetailsPage(this.post);
                Navigation.PushAsync(userDetailsPage);
            }, 0, 0);
            
            ToolbarItems.Add(toolbarItem);
        }

        private void CommentsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {


                Comment comment = e.SelectedItem as Comment;
                Debug.WriteLine("Item: " + comment.Id);
                
                Device.OpenUri(new Uri($"mailto:{comment.Email}"));
                

            }

            // Clear selection
            CommentsListView.SelectedItem = null;
        }
    }
}