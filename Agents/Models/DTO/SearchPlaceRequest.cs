namespace BasalX.Service.Agents.Models.DTO;

/// <summary>
/// Search Criteria request obeject
/// </summary>
/// <param name="location">The location or area you want to base your search on</param>
/// <param name="search_service_type">The service you would like to search on e.i Bars and Pubs, plumbers etc</param>
internal record SearchPlaceRequest(string location, string search_service_type);

