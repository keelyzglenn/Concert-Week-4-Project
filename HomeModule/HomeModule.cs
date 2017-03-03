using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Concert
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                return View["index.cshtml"];
            };

            Get["/venues"] = _ => {
                List<Venue> AllVenues = Venue.GetAll();
                return View["venues.cshtml", AllVenues];
            };

            Get["/venue/new"] = _ => {
                return View["venue_new.cshtml"];
            };

            Post["/venue/new"] = _ => {
                Venue newVenue = new Venue(Request.Form["venue-name"]);
                newVenue.Save();
                List<Venue> AllVenues = Venue.GetAll();
                return View["venues.cshtml", AllVenues];
            };

            Get["venues/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Venue SelectedVenue = Venue.Find(parameters.id);
                List<Band> VenueBands = SelectedVenue.GetBands();
                List<Band> AllBands = Band.GetAll();
                model.Add("venue", SelectedVenue);
                model.Add("venueBands", VenueBands);
                model.Add("allBands", AllBands);
                return View["venue.cshtml", model];
            };

            Post["venue/add_band"] = _ => {
                Venue venue = Venue.Find(Request.Form["venue-id"]);
                Band band = Band.Find(Request.Form["band-id"]);
                venue.AddBand(band);
                List<Venue> AllVenues = Venue.GetAll();
                return View["venues.cshtml", AllVenues];
            };

            Patch["venues/{id}/updated"] = parameters => {
                Venue SelectedVenue = Venue.Find(parameters.id);
                SelectedVenue.Update(Request.Form["new-name"]);
                List<Venue> AllVenues = Venue.GetAll();
                return View["venues.cshtml", AllVenues];
            };

            Delete["venues/{id}/delete"] = parameters => {
              Venue SelectedVenue = Venue.Find(parameters.id);
              SelectedVenue.Delete();
              List<Venue> AllVenues = Venue.GetAll();
              return View["venues.cshtml", AllVenues];
          };

            // for bands
            Get["/bands"] = _ => {
                List<Band> AllBands = Band.GetAll();
                return View["bands.cshtml", AllBands];
            };

            Get["/band/new"] = _ => {
                return View["band_new.cshtml"];
            };

            Post["/band/new"] = _ => {
                Band newBand = new Band(Request.Form["band-name"]);
                newBand.Save();
                List<Band> AllBands = Band.GetAll();
                return View["bands.cshtml", AllBands];
            };

            Get["bands/{id}"] = parameters => {
              Dictionary<string, object> model = new Dictionary<string, object>();
              Band SelectedBand = Band.Find(parameters.id);
              List<Venue> BandVenues = SelectedBand.GetVenues();
              List<Venue> AllVenues = Venue.GetAll();
              model.Add("band", SelectedBand);
              model.Add("bandVenues", BandVenues);
              model.Add("allVenues", AllVenues);
              return View["band.cshtml", model];
          };

            Post["/band/add_venue"] = _ => {
                Band band = Band.Find(Request.Form["band-id"]);
                Venue venue = Venue.Find(Request.Form["venue-id"]);
                band.AddVenue(venue);
                List<Band> AllBands = Band.GetAll();
                return View["bands.cshtml", AllBands];
            };


            Post["/band/delete"] = _ => {
              Band.DeleteAll();
              List<Band> AllBands = Band.GetAll();
              return View["bands.cshtml", AllBands];
          };
        }
    }
}
