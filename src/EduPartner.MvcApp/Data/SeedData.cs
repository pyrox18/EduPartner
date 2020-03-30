﻿using EduPartner.MvcApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EduPartner.MvcApp.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<EduPartnerDbContext>())
            {
                var parent = new Parent
                {
                    Id = Guid.NewGuid(),
                    Name = "Ali bin Abu",
                    Email = "ali@test.com",
                    Password = "abc123",
                    Address = "1, Jalan 2, 40000 Shah Alam, Selangor"
                };

                context.Parents.Add(parent);

                var childOne = new Child
                {
                    Id = Guid.NewGuid(),
                    Name = "Ahmad bin Ali",
                    DateOfBirth = new DateTime(2013, 1, 1),
                    Parent = parent
                };

                var childTwo = new Child
                {
                    Id = Guid.NewGuid(),
                    Name = "Aishah binti Ali",
                    DateOfBirth = new DateTime(2010, 1, 1),
                    Parent = parent
                };

                context.Children.AddRange(childOne, childTwo);

                var teacherOne = new Teacher
                {
                    Id = Guid.NewGuid(),
                    Name = "Fatimah binti Zaid",
                    PhoneNumber = "60192223333"
                };

                var teacherTwo = new Teacher
                {
                    Id = Guid.NewGuid(),
                    Name = "Abdul Aziz bin Rahim",
                    PhoneNumber = "60123334444"
                };

                var teacherThree = new Teacher
                {
                    Id = Guid.NewGuid(),
                    Name = "Siti Hawa binti Aiman",
                    PhoneNumber = "60164445555"
                };

                context.Teachers.AddRange(teacherOne, teacherTwo, teacherThree);

                var mathSubject = new Subject
                {
                    Id = Guid.NewGuid(),
                    Name = "Mathematics",
                    MonthlyFee = 150.00m,
                    Teachers = new List<Teacher> { teacherOne }
                };

                var scienceSubject = new Subject
                {
                    Id = Guid.NewGuid(),
                    Name = "Science",
                    MonthlyFee = 170.00m,
                    Teachers = new List<Teacher> { teacherTwo, teacherThree }
                };

                context.Subjects.AddRange(mathSubject, scienceSubject);

                var enrollment = new Enrollment
                {
                    Id = Guid.NewGuid(),
                    Child = childOne,
                    Subject = mathSubject,
                    Teacher = teacherOne,
                    TimeslotDayOfWeek = DayOfWeek.Monday,
                    TimeslotTime = new DateTime(2020, 1, 1, 14, 0, 0)
                };

                context.Enrollments.Add(enrollment);

                context.SaveChanges();
            }
        }
    }
}
