using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ITower.Level_Assets
{
    public static class SharedMapLogic
    {
        static SharedMapLogic() 
        {
            trueMousePosition = new Godot.Vector2()
            {
                x = 0,
                y = 0
            };
        }
        public static Godot.Vector2 trueMousePosition;
        
    }
}
