using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Puzzle
{
    public class ModalPage : ContentPage
    {

        public int ID { get; set; }

        public string Term { get; set; }

        public string Defenition { get; set; }

        public string ImageFilename { get; set; }
        Entry termEntry = new Entry();
        Entry definitionEntry = new Entry();

        public ModalPage()
        {
        }

        public void PreprateModalData()
        {
            termEntry = new Entry
            {
                Placeholder = "Term",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };


            definitionEntry = new Entry
            {
                Placeholder = "Definition",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            if (BindingContext != null)
            {
                ID = ((DataSource)BindingContext).ID;
                termEntry.Text = ((DataSource)BindingContext).Term;
                definitionEntry.Text = ((DataSource)BindingContext).Defenition;
                ImageFilename = ((DataSource)BindingContext).ImageFilename;
            }

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += OnSaveButtonClicked;

            var dismissButton = new Button { Text = "Dismiss" };
            dismissButton.Clicked += OnDismissButtonClicked;

            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        Children = {termEntry}
                    },
                    new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        Children = { definitionEntry }
                    },
                    new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        Children = {saveButton,dismissButton }
                    }
                }
            };

        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {

            await Navigation.PopModalAsync(true);
        }

        async void OnSaveButtonClicked(object sender, EventArgs args)
        {

            if (BindingContext != null)
            {
                ID = ((DataSource)BindingContext).ID;
                ImageFilename = ((DataSource)BindingContext).ImageFilename;

                DataSource.EditGlossaryTerm(ID, termEntry.Text, definitionEntry.Text, ImageFilename);
            }
            else
            {
                DataSource.AddGlossaryTerm(termEntry.Text, definitionEntry.Text, ImageFilename);
            }
            MainContentPage.RefreshListData();

            await Navigation.PopModalAsync(true);
        }

        //private void DismissButton_Clicked(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void SaveButton_Clicked(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
