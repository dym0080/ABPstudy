﻿using System.Collections.Generic;
using System.Linq;
using Abp.Localization;
using SNAS.EntityFramework;

namespace SNAS.Migrations.SeedData
{
    public class DefaultLanguagesBuilder
    {
        public static List<ApplicationLanguage> InitialLanguages { get; private set; }

        private readonly SNASDbContext _context;

        static DefaultLanguagesBuilder()
        {
            InitialLanguages = new List<ApplicationLanguage>
            {
                new ApplicationLanguage(null, "en", "English", "famfamfam-flag-gb"),
                new ApplicationLanguage(null, "tr", "Türkçe", "famfamfam-flag-tr"),
                new ApplicationLanguage(null, "zh-CN", "简体中文", "famfamfam-flag-cn"),
                new ApplicationLanguage(null, "pt-BR", "Português-BR", "famfamfam-flag-br")
            };
        }

        public DefaultLanguagesBuilder(SNASDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialLanguages)
            {
                AddLanguageIfNotExists(language);
            }
        }

        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
            {
                return;
            }

            _context.Languages.Add(language);

            _context.SaveChanges();
        }
    }
}
