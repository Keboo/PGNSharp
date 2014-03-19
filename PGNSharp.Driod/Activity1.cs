using System.IO;
using System.Text;
using Android.App;
using Android.OS;
using Android.Widget;
using PGNSharp.Core;
using PGNSharp.Driod.Resources;

namespace PGNSharp.Driod
{
    [Activity(Label = "PGNSharp.Driod", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private Game _game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(TestData.TestGame.PGN)))
            {
                _game = Game.Load(stream);
            }
            // Set our view from the "main" layout resource
            SetContentView(Resource.Id.gridView1);

            var gridView = FindViewById<GridView>(Resource.Id.gridView1);
            gridView.Adapter = new BoardAdapter(this, _game);
            //ListView.Adapter = new BoardAdapter(this, _game);

            // Get our button from the layout resource,
            // and attach an event to it
        }
    }
}

