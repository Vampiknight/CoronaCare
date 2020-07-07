using System;
using System.Net;
using Android;
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

namespace CoronaCare
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        //protected int m_nKlicks = 1;
        //protected int m_nKlicks2 = 1;

        protected string m_körpertemp;
        protected string m_datum;
        protected bool m_unterschrift;
        protected string webseite;
        protected int counterCheck;

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
            // Button myButton = (Button)sender;
            // myButton.Text = string.Format("{0} clicks bereits!", m_nKlicks++);

            TextView körpertemp = FindViewById<TextView>(Resource.Id.txtKörpertemp);
            TextView datum = FindViewById<TextView>(Resource.Id.txtDatum);
            CheckBox unterschrift = FindViewById<CheckBox>(Resource.Id.checkEinwilligung);

            m_körpertemp = körpertemp.Text;
            m_datum = datum.Text;
            m_unterschrift = unterschrift.Checked;

            webseite = "http://10.0.2.2:51979/myTemp?temp=" + m_körpertemp + "&datum=" + m_datum + "&unterschrift=" + m_unterschrift;

            //TextView textWillkommen = FindViewById<TextView>(Resource.Id.textwillkommen);
            //textWillkommen.Text = webseite;

            WebRequest request = WebRequest.Create(webseite);
            request.Method = "POST";
            try
            {
                WebResponse response = request.GetResponse();
            }
            catch (Exception e)
            {
                //textWillkommen.Text = e.Message;
            }
        }
        //test
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

            counterCheck = 0;

            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            RelativeLayout rl = FindViewById<RelativeLayout>(Resource.Id.content);
            rl.RemoveAllViews();
            inflater.Inflate(Resource.Layout.health_evaluation, rl);

            if (m_bFieber == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                textDeineSymptome.Text = "Fieber, ";
                counterCheck++;
            }
            if (m_bHusten == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "trockener Husten, ";
                counterCheck++;
            }
            if (m_bMüdigkeit == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Müdigkeit, ";
                counterCheck++;
            }
            if (m_bGeschmack == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Verlust des Geschmackssinns, ";
                counterCheck++;
            }
            if (m_bGeruch == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Verlust des Geruchssinns, ";
                counterCheck++;
            }
            if (m_bAtem == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Atembeschwerden, ";
                counterCheck++;
            }
            if (m_bDruck == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Druckgefühl im Brustbereich, ";
                counterCheck++;
            }
            if (m_bKopfschmerzen == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Kopfschmerzen, ";
                counterCheck++;
            }
            if (m_bDurchfall == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Durchfall, ";
                counterCheck++;
            }
            if (m_bGlieder == true)
            {
                TextView textDeineSymptome = FindViewById<TextView>(Resource.Id.textDeineSymptome);
                string s = textDeineSymptome.Text;
                textDeineSymptome.Text = s + "Gliederschmerzen";
                counterCheck++;
            }
            
            if(counterCheck == 0)
            {
                TextView textDeineRatschlaege = FindViewById<TextView>(Resource.Id.textView4);
                textDeineRatschlaege.Text = "Sie sind gesund.";
            }

            if (counterCheck == 1)
            {
                TextView textDeineRatschlaege = FindViewById<TextView>(Resource.Id.textView4);
                textDeineRatschlaege.Text = "Sie müssen sich schonen und bei wiederholten auftreten des Symptoms, den Arzt konsultieren.";
            }

            if (counterCheck == 2)
            {
                TextView textDeineRatschlaege = FindViewById<TextView>(Resource.Id.textView4);
                textDeineRatschlaege.Text = "Schonen Sie sich und konsultieren sie ihren Arzt bei Verschlechterung der Symptome.";
            }

            if (counterCheck == 3)
            {
                TextView textDeineRatschlaege = FindViewById<TextView>(Resource.Id.textView4);
                textDeineRatschlaege.Text = "Es wird geraten schnellstmöglich ihren Hausarzt zu konsultieren.";
            }

            if (counterCheck >= 4)
            {
                TextView textDeineRatschlaege = FindViewById<TextView>(Resource.Id.textView4);
                textDeineRatschlaege.Text = "Gehen sie umgehend zu ihren Hausarzt, die Angelegenheit ist kritisch und sollte untersucht werden.";
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

                // Button myButtonCheck = FindViewById<Button>(Resource.Id.button1);
                // myButtonCheck.Click += OnMyButtonClicked;
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

