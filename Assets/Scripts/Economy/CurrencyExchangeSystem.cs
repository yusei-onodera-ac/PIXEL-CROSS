using PixelCross.Data;

namespace PixelCross.Economy
{
    // One-way exchange only: premium currency can buy basic currency, but
    // basic currency can never be converted back (keeps IAP the only source
    // of premium currency other than the login bonus).
    public static class CurrencyExchangeSystem
    {
        public const int PremiumToBasicRate = 10;

        public static bool ExchangePremiumForBasic(TeamData team, int premiumAmountToSpend)
        {
            if (premiumAmountToSpend <= 0 || team.PremiumCurrency < premiumAmountToSpend)
            {
                return false;
            }

            team.PremiumCurrency -= premiumAmountToSpend;
            team.BasicCurrency += premiumAmountToSpend * PremiumToBasicRate;
            return true;
        }
    }
}
