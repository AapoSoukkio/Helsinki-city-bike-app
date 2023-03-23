namespace Solita.HelsinkiBikeApp.Server.Helpers
{
    public class DataValidator
    {
        public static bool ValidateData(DateTime? departureDate, int pageNumber, int pageSize, out string errorMessage)
        {
            if (departureDate.HasValue && departureDate.Value.Year != 2021)
            {
                errorMessage = "Invalid departure date year";
                return false;
            }

            if (pageNumber < 1 || pageNumber > 100000)
            {
                errorMessage = "Invalid page number";
                return false;
            }

            if (pageSize < 0 || pageSize > 1000)
            {
                errorMessage = "Invalid page size";
                return false;
            }

            // Data is valid
            errorMessage = null;
            return true;
        }

    }
}
