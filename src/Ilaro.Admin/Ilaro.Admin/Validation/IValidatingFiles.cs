﻿using Ilaro.Admin.Core;

namespace Ilaro.Admin.Validation
{
    public interface IValidatingFiles
    {
        bool Validate(Property property);
    }
}