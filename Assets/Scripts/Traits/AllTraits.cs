public class AllTraits
{
    public static readonly Trait Sickly = new Trait
    (
        "Sickly",
        "Desc",
        (data) => { data.AdditionalLevel -= 1; },
        (data) => { data.AdditionalLevel += 1; },
        (minister) => minister.Boredom < 0);
}
