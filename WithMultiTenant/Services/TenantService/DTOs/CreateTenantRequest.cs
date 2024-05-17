﻿using Microsoft.AspNetCore.Authentication;

namespace WithMultiTenant.Services.TenantService.DTOs
{
    public class CreateTenantRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Isolated { get; set; }
    }
}
