namespace UKP.Website.Application
{
    public interface IConfiguration
    {
        string IasBaseUrl { get; }
        string IasAuthKey { get; }
        string MemberAutocompleteApi { get; }
    }
}