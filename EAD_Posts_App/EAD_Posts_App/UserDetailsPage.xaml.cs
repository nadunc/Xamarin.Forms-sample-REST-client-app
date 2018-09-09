using EAD_Posts_App.Data;
using EAD_Posts_App.Net;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EAD_Posts_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserDetailsPage : ContentPage
	{

        private Post post;
        DataRetriever _dr;
        User User;

		public UserDetailsPage()
		{
			InitializeComponent();
            
        }

        public UserDetailsPage(Post post) : this()
        {
            this.post = post;
            Title = this.post.UserId+"'s Profile";

            _dr = new DataRetriever();
            LoadData();
        }

        private void LoadData()
        {

            Task.Factory.StartNew(() => {
                User = _dr.GetUserById(this.post.UserId);
                
            }).ContinueWith(task => {
                BindingContext = User;
                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}