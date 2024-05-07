using SoldierInfoContext;
using ValorVault.Models;

public interface ISoldierInfoService
{
    void AddSoldierInfo(SoldierInfo soldierInfo);
    void UpdateSoldierInfo(SoldierInfo soldierInfo);
    void DeleteSoldierInfo(int id);
    SoldierInfo GetSoldierInfoById(int id);
}

public class SoldierInfoService : ISoldierInfoService
{
    private readonly SoldierInfoDbContext _context;

    public SoldierInfoService(SoldierInfoDbContext context)
    {
        _context = context;
    }

    public void AddSoldierInfo(SoldierInfo soldierInfo)
    {
        _context.soldier_infos.Add(soldierInfo);
        _context.SaveChanges();
    }

    public void UpdateSoldierInfo(SoldierInfo soldierInfo)
    {
        _context.soldier_infos.Update(soldierInfo);
        _context.SaveChanges();
    }

    public void DeleteSoldierInfo(int id)
    {
        var soldierInfo = _context.soldier_infos.Find(id);
        if (soldierInfo != null)
        {
            _context.soldier_infos.Remove(soldierInfo);
            _context.SaveChanges();
        }
    }

    public SoldierInfo GetSoldierInfoById(int id)
    {
        return _context.soldier_infos.Find(id);
    }
}
