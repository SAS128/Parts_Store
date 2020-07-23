using PartsStore.Models;
using PartsStore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PartsStore.Pages.Admin
{
    public partial class Details : System.Web.UI.Page
    {
        private Repository repository = new Repository();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Detail> GetDetails() => repository.Details;


        public void UpdateDetails(int gameID)
        {
            Detail myGame = repository.Details.Where(p => p.DetailsId == gameID).FirstOrDefault();
            if (myGame != null && TryUpdateModel(myGame, new FormValueProvider(ModelBindingExecutionContext)))
            {
                repository.SaveDetails(myGame);
            }
        }

        public void DeleteDetails(int gameID)
        {
            Detail myGetDetail = repository.Details.Where(p => p.DetailsId == gameID).FirstOrDefault();
            if (myGetDetail != null)
            {
                repository.DeleteDetail(myGetDetail);
            }
        }
        public void InsertDetails()
        {
            Detail game = new Detail();
            if (TryUpdateModel(game, new FormValueProvider(ModelBindingExecutionContext)))
            {
                repository.SaveDetails(game);
            }
        }

    }
}