using System;
using System.Collections.Generic;
using System.Text;
using Qm.Lims.Localization;
using Volo.Abp.Application.Services;

namespace Qm.Lims
{
    /* Inherit your application services from this class.
     */
    public abstract class LimsAppService : ApplicationService
    {
        protected LimsAppService()
        {
            LocalizationResource = typeof(LimsResource);
        }
    }
}
