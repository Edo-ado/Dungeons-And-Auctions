namespace D_A.Web.Models.Api
{
    public record UserApiDto
    {
            public int Id { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string RoleName { get; set; } = string.Empty;
            public string CountryName { get; set; } = string.Empty;
            public string GenderName { get; set; } = string.Empty;
            public bool IsBlocked { get; set; }
            public bool Active { get; set; }
            public int NumberCreatedAuctions { get; set; }
            public int NumberBidMade { get; set; }
        
    }
}
