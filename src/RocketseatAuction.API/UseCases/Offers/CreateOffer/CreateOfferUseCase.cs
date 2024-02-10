using RocketseatAuction.API.Communication.Requests;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Services;

namespace RocketseatAuction.API.UseCases.Offers.CreateOffer;

public class CreateOfferUseCase
{
    private readonly ILoggedUser _loggerUser;
    private readonly IOfferRepository _offerRepository;

    public CreateOfferUseCase(ILoggedUser loggedUser, IOfferRepository offerRepository)
    {
        _loggerUser = loggedUser;
        _offerRepository = offerRepository;
    }

    public int Execute(int itemId, RequestCreateOfferJson request)
    {
        var user = _loggerUser.User();

        // var today = DateTime.Now;

        var offer = new Offer
        {
            CreatedOn = DateTime.Now,
            ItemId = itemId,
            Price = request.Price,
            UserId = user.Id,
        };

        _offerRepository.Add(offer);

        return offer.Id;
    }
}
