namespace Labinator2016.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Lib.Headers;
    using Lib.Models;
    using ViewModels.DatatablesViewModel;
    public class ClassroomsController : Controller
    {
        /// <summary>
        /// Handle to the database
        /// </summary>
        private ILabinatorDb db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassroomsController"/> class.
        /// Used for regular constructions. Obtains handle to Database.
        /// </summary>
        public ClassroomsController()
        {
            this.db = new LabinatorContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassroomsController"/> class.
        /// Used for constructing when Unit Testing.
        /// </summary>
        /// <param name="db">Handle to Database stub.</param>
        public ClassroomsController(ILabinatorDb db)
        {
            this.db = db;
        }

        /// <summary>
        /// Used to respond to an AJAX request for a list of Classrooms. The results are then
        /// used to populate a DataTable on the Index Page.
        /// </summary>
        /// <param name="param">The information sent from the DataTable regarding sorts, filters etc.</param>
        /// <returns>A JSON response with the new information to display.</returns>
        [AllowAnonymous]
        public ActionResult Ajax(DTParameters param)
        {
            return this.Json(Generic.Ajax<Classroom>(this.db.Query<Classroom>().ToList(), param));
        }
    }
}