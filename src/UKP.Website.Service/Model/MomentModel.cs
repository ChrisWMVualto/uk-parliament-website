namespace UKP.Website.Service.Model
{
    public class MomentModel
    {
        public int Number { get; private set; }
        public string Title { get; private set; }
        public string Url { get; private set; }

        public MomentModel(int number, string title, string url)
        {
            Title = title;
            Url = url;
        }
    }
}
