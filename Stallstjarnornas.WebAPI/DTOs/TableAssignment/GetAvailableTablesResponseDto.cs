namespace Stallstjarnornas.WebAPI.DTOs.TableAssignment
{
    public record GetAvailableTablesResponseDto(

       DateOnly bookingDate,
       int SittingId,
       List<int> TableIds//Admin ska kunna assigna flera bord för en bokning

     );
}
