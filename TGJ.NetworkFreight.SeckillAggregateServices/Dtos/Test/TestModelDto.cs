﻿namespace TGJ.NetworkFreight.SeckillAggregateServices.Dtos.Test
{
	public class TestModelDto
    {
        public virtual string SayLoveU()
        {
            return "I ♥ U";
        }
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
    }
}