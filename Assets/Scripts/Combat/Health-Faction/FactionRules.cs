public static class FactionRules
{
    public static bool CanDamage(Faction attacker, Faction target)
    {
        // Same faction → no damage
        if (attacker == target)
            return false;

        // Player cannot damage Nexus
        if (attacker == Faction.Player && target == Faction.Nexus)
            return false;

        // Enemy can damage Player and Nexus
        if (attacker == Faction.Enemy && (target == Faction.Player || target == Faction.Nexus))
            return true;

        // Player can damage Enemy
        if (attacker == Faction.Player && target == Faction.Enemy)
            return true;

        // Neutral can damage anything
        if (attacker == Faction.Neutral)
            return true;

        // Default: block damage
        return false;
    }
}
