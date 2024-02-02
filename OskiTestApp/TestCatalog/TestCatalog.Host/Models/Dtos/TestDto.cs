﻿using TestCatalog.Host.Data.Entities;

namespace TestCatalog.Host.Models.Dtos
{
    public class TestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionEntity> Questions { get; set; }
    }
}