using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Interfaces
{
    public interface IInitializable
    {
        public void Initialize(Initializer initializer);
    }
}
