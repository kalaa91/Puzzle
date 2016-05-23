using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Puzzle
{
    public class MainContentPage : ContentPage
    {
        static DataSource Source = new DataSource();
        static ListView listView = new ListView();
        public MainContentPage()
        {
            Source.GetGlossaryList();

            Label header = new Label
            {
                Text = "Glossary Puzzle",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Button newGlossary = new Button
            {
                Text = "Add New Glossary"
            };
            newGlossary.Clicked += NewGlossary_Clicked;

            SearchBar searchBar = new SearchBar();
            searchBar.TextChanged += SearchBar_TextChanged;
            listView = new ListView
            {
                ItemsSource = Source.GlossaryList,
                //IsGroupingEnabled = true,
                //GroupDisplayBinding = new Binding("Term"),
                GroupShortNameBinding = new Binding("Term"),
                ItemTemplate = new DataTemplate(() =>
                        {

                        // Create views with bindings for displaying each property.
                        Label termLabel = new Label();
                        //termLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                        termLabel.SetBinding(Label.TextProperty, "Term");

                            Label defenitionLabel = new Label();
                            defenitionLabel.SetBinding(Label.TextProperty, "Defenition");
                            defenitionLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
                            defenitionLabel.VerticalOptions= LayoutOptions.FillAndExpand;
                            defenitionLabel.LineBreakMode = LineBreakMode.NoWrap;

                            BoxView boxView = new BoxView();
                            boxView.SetBinding(BoxView.ColorProperty, "Red");

                            ScrollView scrollViewHeader = new ScrollView
                            {
                                HeightRequest = 600,
                                Content = defenitionLabel
                            };

                            List<View> ViewCellItems = new List<View>();

                            ViewCellItems.Add(
                                new StackLayout
                                {
                                //Padding = new Thickness(0, 5),
                                Orientation = StackOrientation.Horizontal,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    VerticalOptions = LayoutOptions.FillAndExpand,
                                    Children =
                                        {
                                    boxView,
                                    new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.FillAndExpand,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Spacing = 3,
                                        Children =
                                        {
                                            termLabel,
                                            scrollViewHeader
                                        }
                                    }
                                        }
                                });



                            var editAction = new MenuItem { Text = "Edit" };
                            editAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
                            editAction.Clicked += EditAction_Clicked;

                            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
                        deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
                            deleteAction.Clicked += DeleteAction_Clicked; ;

                            CustomViewCell MyCustomViewCell = new CustomViewCell(ViewCellItems);
                            MyCustomViewCell.ContextActions.Add(editAction);
                            MyCustomViewCell.ContextActions.Add(deleteAction);

                            return MyCustomViewCell;
                        }),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            listView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };


            listView.IsPullToRefreshEnabled = true;
            listView.Refreshing += ListView_Refreshing;
            Content = new StackLayout
            {
                Padding = 10,
                Spacing = 10,
                Children = { header, newGlossary, searchBar, listView },
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string SearchQuery =  ((SearchBar)sender).Text;
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                Source.GetGlossaryList(SearchQuery);
                listView.ItemsSource = Source.GlossaryList;
            }
            else
            {
                Source.GetGlossaryList();
                listView.ItemsSource = Source.GlossaryList;
            }

        }

        void NewGlossary_Clicked(object sender, EventArgs e)
        {
            //open the add new popup
            var detailPage = new ModalPage();
            detailPage.PreprateModalData();
            Navigation.PushModalAsync(detailPage, true);
            RefreshListData();
        }

        void EditAction_Clicked(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;

            //get the item id and open the popup and populate the data on it
            var detailPage = new ModalPage();
            detailPage.BindingContext = item.CommandParameter as DataSource;
            detailPage.PreprateModalData();
            //listView.SelectedItem = null;
            Navigation.PushModalAsync(detailPage, true);
            RefreshListData();
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            RefreshListData();
            listView.IsRefreshing = false;
        }

        private void DeleteAction_Clicked(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            if (item != null && (DataSource)item.CommandParameter != null)
            {
                Source.DeleteGlossaryTerm(((DataSource)item.CommandParameter).ID);
                RefreshListData();
            }

        }

        public static void RefreshListData()
        {
            Source.GetGlossaryList();
            listView.ItemsSource = Source.GlossaryList;

        }
    }
}
