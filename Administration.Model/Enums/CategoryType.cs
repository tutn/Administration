using System;
using System.ComponentModel;
using System.Linq;

namespace Administration.Model.Enums
{
    public enum CategoryType : int
    {
        [Description("International")]
        International = 1,
        [Description("Domestic")]
        Domestic = 2,
    }

    public enum Source : int
    {
        [Description("International")]
        International = 1,
        [Description("VietFood")]
        VietFood = 2,
    }

    public enum CategoryGroup : int
    {
        [Description("CattleHogs")]
        CattleHogs = 3,
        [Description("Currency")]
        Currency = 7,
        [Description("FoodFiber")]
        FoodFiber = 18,
        [Description("GrainOilSeed")]
        GrainOilSeed = 25,
        [Description("Index")]
        Index = 36,
        [Description("Interest")]
        Interest = 43,
        [Description("Metal")]
        Metal = 50,
        [Description("OilEnergy")]
        OilEnergy = 56,
    }

    public enum WeightType: int
    {
        [Description("Tons")]
        Tons = 1,
        [Description("Kilograms")]
        Kilograms = 2,
    }

    public enum PackingType : int
    {
        [Description("Fresh")]
        Fresh = 1,
        [Description("Frozen")]
        Frozen = 2,        
    }

    public enum DimensionType : int
    {
        [Description("Grams")]
        Grams = 1,
        [Description("Kilograms")]
        Kilograms = 2,
        [Description("Centimeters")]
        Centimeters = 3,
        [Description("Con")]
        Con = 4,
    }
}
