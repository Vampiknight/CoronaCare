using System;
using System.Net;
using System.Reflection;
using Android;
using System.IO;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Org.Apache.Http.Protocol;

namespace CoronaCare
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        protected int m_nKlicks = 1;
        protected int m_nKlicks2 = 1;

        protected string m_körpertemp;
        protected string m_datum;
        protected bool m_unterschrift;
        protected string webseite;

        protected bool m_bFieber = false;
        protected bool m_bHusten = false;
        protected bool m_bMüdigkeit = false;
        protected bool m_bGeschmack = false;
        protected bool m_bGeruch = false;
        protected bool m_bAtem = false;
        protected bool m_bDruck = false;
        protected bool m_bKopfschmerzen = false;
        protected bool m_bDurchfall = false;
        protected bool m_bGlieder = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            Button myButton = FindViewById<Button>(Resource.Id.btnübermitteln);
            myButton.Click += OnMyButtonClicked;
        }

        void OnMyButtonClicked(object sender, EventArgs args)
        {
            //Button myButton = (Button)sender;
            //myButton.Text = string.Format("{0} clicks bereits!", m_nKlicks++);

            TextView körpertemp = FindViewById<TextView>(Resource.Id.txtKörpertemp);
            TextView datum = FindViewById<TextView>(Resource.Id.txtDatum);
            CheckBox unterschrift = FindViewById<CheckBox>(Resource.Id.checkEinwilligung);

            m_körpertemp = körpertemp.Text;
            m_datum = datum.Text;
            m_unterschrift = unterschrift.Checked;

            webseite = "http://localhost:50889/myTemp?körpertemp=" + m_körpertemp + "&datum=" + m_datum + "&unterschrift=" + m_unterschrift;

            TextView textWillkommen = FindViewById<TextView>(Resource.Id.textwillkommen);           
            textWillkommen.Text = webseite;

            WebRequest request = WebRequest.Create("http://localhost:54398/myTemp?temp=39&datum=28.06.2020&unterschrift=true");
            request.Method = "POST";
            WebResponse response = request.GetResponse();
            
        }
        
        void OnMyButtonClickedCheck(object sender, EventArgs args)
        {
            CheckBox myCheckFieber = FindViewById<CheckBox>(Resource.Id.checkFieber);
            CheckBox myCheckHusten = FindViewById<CheckBox>(Resource.Id.checkHusten);
            CheckBox myCheckMüdigkeit = FindViewById<CheckBox>(Resource.Id.checkMüdigkeit);
            CheckBox myCheckGeschmack = FindViewById<CheckBox>(Resource.Id.checkGeschmack);
            CheckBox myCheckGeruch = FindViewById<CheckBox>(Resource.Id.checkGeruchssinn);
            CheckBox myCheckAtem = FindViewById<CheckBox>(Resource.Id.checkAtem);
            CheckBox myCheckDruck = FindViewById<CheckBox>(Resource.Id.checkDruckgefühl);
            CheckBox myCheckKopfschmerzen = FindViewById<CheckBox>(Resource.Id.checkKopfschmerzen);
            CheckBox myCheckDurchfall = FindViewById<CheckBox>(Resource.Id.checkDurchfall);
            CheckBox myCheckGlieder = FindViewById<CheckBox>(Resource.Id.checkGlieder);

            m_bFieber = myCheckFieber.Checked;
            m_bHusten = myCheckHusten.Checked;
            m_bMüdigkeit = myCheckMüdigkeit.Checked;
            m_bGeschmack = myCheckGeschmack.Checked;
            m_bGeruch = myCheckGeruch.Checked;
            m_bAtem = myCheckAtem.Checked;
            m_bDruck = myCheckDruck.Checked;
            m_bKopfschmerzen = myCheckKopfschmerzen.Checked;
            m_bDurchfall = myCheckDurchfall.Checked;
            m_bGlieder = myCheckGlieder.Checked;

            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            RelativeLayout rl = FindViewById<RelativeLayout>(Resource.Id.content);
            rl.RemoveAllViews();
            inflater.Inflate(Resource.Layout.health_evaluation, rl);

            if (m_bFieber == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                textDeineSymptome.Text = "Fieber, ";
            }
            if (m_bHusten == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "trockener Husten, ";
            }
            if (m_bMüdigkeit == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Müdigkeit, ";
            }
            if (m_bGeschmack == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Verlust des Geschmackssinns, ";
            }
            if (m_bGeruch == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Verlust des Geruchssinns, ";
            }
            if (m_bAtem == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Atembeschwerden, ";
            }
            if (m_bDruck == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Druckgefühl im Brustbereich, ";
            }
            if (m_bKopfschmerzen == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Kopfschmerzen, ";
            }
            if (m_bDurchfall == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Durchfall, ";
            }
            if (m_bGlieder == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Gliederschmerzen";
            }
        }
        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                RelativeLayout rl = FindViewById<RelativeLayout>(Resource.Id.content);
                rl.RemoveAllViews();
                inflater.Inflate(Resource.Layout.health_check, rl);
               
                Button myButtonCheck = FindViewById<Button>(Resource.Id.buttonCheck);
                myButtonCheck.Click += OnMyButtonClickedCheck;                  
            }
            else if (id == Resource.Id.nav_gallery)
            {
                var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                RelativeLayout rl = FindViewById<RelativeLayout>(Resource.Id.content);
                rl.RemoveAllViews();
                inflater.Inflate(Resource.Layout.content_main, rl);

                //Button myButtonCheck = FindViewById<Button>(Resource.Id.button1);
                //myButtonCheck.Click += OnMyButtonClicked;
            }
            else if (id == Resource.Id.nav_slideshow)
            {
                var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                RelativeLayout rl = FindViewById<RelativeLayout>(Resource.Id.content);
                rl.RemoveAllViews();
                inflater.Inflate(Resource.Layout.health_evaluation, rl);
            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

