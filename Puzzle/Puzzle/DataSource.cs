using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Puzzle
{
    public class DataSource
    {
        public int ID { get; set; }

        public string Term { get; set; }

        public string Defenition { get; set; }

        public string ImageFilename { get; set; }

        public List<DataSource> GlossaryList { get; set; }

        static List<DataSource> DataList { get; set; }

        public DataSource()
        {
            GlossaryList = new List<DataSource>();
            PopulateList();
            GlossaryList.AddRange(DataList);
            GlossaryList.AddRange(GlossaryList.Distinct().ToList());
        }

        public DataSource(int id, string term, string defenition, string imageFilename)
        {
            ID = id;
            Term = term;
            Defenition = defenition;
            ImageFilename = imageFilename;
            //GlossaryList = new List<DataSource>();
            //PopulateList();
            //GlossaryList.AddRange(GlossaryList);
        }

        public void GetGlossaryList(string SearchTerm = null)
        {
            GlossaryList = new List<DataSource>();
            //GlossaryList.AddRange(DataList);

            if (SearchTerm == null)
            {

                var sorted = DataList.OrderBy(a => a.Term).ToList();
                GlossaryList.AddRange(sorted);
            }
            else
            {
                var sorted = DataList.Where(a => a.Term.ToLower().Contains(SearchTerm.ToLower())).ToList();
                GlossaryList.AddRange(sorted);
            }
        }

        public static void AddGlossaryTerm(string term, string defenition, string imageFilename)
        {
            int NewID = DataList[DataList.Count - 1].ID + 1;
            DataList.Add(new DataSource(NewID, term, defenition, imageFilename));
        }

        public static void EditGlossaryTerm(int id, string term, string defenition, string imageFilename)
        {
            DataSource EditedTerm = DataList.FirstOrDefault(x => x.ID == id);
            if (EditedTerm != null)
            {
                EditedTerm.Term = term;
                EditedTerm.Defenition = defenition;
                EditedTerm.ImageFilename = imageFilename;
            }
        }

        public void DeleteGlossaryTerm(int id)
        {
            DataSource SelectedTerm = DataList.Where(a => a.ID == id).FirstOrDefault();
            if (SelectedTerm != null)
            {
                DataList.Remove(SelectedTerm);
            }
        }

        static void PopulateList()
        {
            try
            {


                if (DataList == null)
                {
                    DataList = new List<DataSource>();
                }

                DataList.Add(new DataSource(1, "abyssal plain", "The ocean floor offshore from the continental margin, usually very flat with a slight slope.", "Vegetables"));
                DataList.Add(new DataSource(2, "accrete", "v. To add terranes (small land masses or pieces of crust) to another, usually larger, land mass.", "Vegetables"));
                DataList.Add(new DataSource(3, "alkaline", "Term pertaining to a highly basic, as opposed to acidic, subtance. For example, hydroxide or carbonate of sodium or potassium.", "Vegetables"));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
