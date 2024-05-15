using SoldierInfoContext;
using ValorVault.Models;

public interface ISoldierInfoService
{
    void AddSoldierInfo(SoldierInfo soldierInfo);
    void UpdateSoldierInfo(SoldierInfo soldierInfo);
    void DeleteSoldierInfo(int id);
    SoldierInfo GetSoldierInfoById(int id);
    List<SoldierInfo> GetSoldiersByName(string name);
    List<SoldierInfo> GetSoldiersByCallSign(string callSign);
    SoldierInfo GetSoldierById(int id);
    List<SoldierInfo> SearchSoldiers(string keyword);
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
    public List<SoldierInfo> GetSoldiersByName(string name)
    {
        return _context.soldier_infos.Where(s => s.soldier_name.Contains(name) && s.profile_status != "Unverified")
            .ToList();
    }

    public List<SoldierInfo> GetSoldiersByCallSign(string callSign)
    {
        return _context.soldier_infos.Where(s => s.call_sign.Contains(callSign) && s.profile_status != "Unverified")
            .ToList();
    }

    public SoldierInfo GetSoldierById(int id)
    {
        return _context.soldier_infos.FirstOrDefault(s => s.soldier_info_id == id);
    }

    public List<SoldierInfo> SearchSoldiers(string keyword)
    {
        return _context.soldier_infos
            .Where(s => s.soldier_name.Contains(keyword) || s.call_sign.Contains(keyword) && s.profile_status != "Unverified")
            .ToList();
    }
}