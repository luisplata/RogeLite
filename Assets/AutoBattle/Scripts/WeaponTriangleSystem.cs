using Items;

public static class WeaponTriangleSystem
{
    public static int GetWeaponTriangleBonus(WeaponType attacker, WeaponType defender)
    {
        if (attacker == WeaponType.Sword && defender == WeaponType.Axe) return +2;
        if (attacker == WeaponType.Axe && defender == WeaponType.Lance) return +2;
        if (attacker == WeaponType.Lance && defender == WeaponType.Sword) return +2;

        if (attacker == WeaponType.Axe && defender == WeaponType.Sword) return -2;
        if (attacker == WeaponType.Lance && defender == WeaponType.Axe) return -2;
        if (attacker == WeaponType.Sword && defender == WeaponType.Lance) return -2;

        return 0;
    }
}