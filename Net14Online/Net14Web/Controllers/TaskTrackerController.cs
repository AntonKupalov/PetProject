﻿using Microsoft.AspNetCore.Mvc;
using Net14Web.DbStuff;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Net14Web.Models.TaskTracker;
using Net14Web.Services;
using System.Xml.Linq;
using Net14Web.DbStuff.Models.TaskTracker;
using Net14Web.DbStuff.Repositories.TaskTracker;

namespace Net14Web.Controllers
{
    public class TaskTrackerController : Controller
    {
        public static List<TaskViewModel> taskViewModels = new List<TaskViewModel>();
        private TaskRepository _taskRepository;
        private AuthService _authService;

        public TaskTrackerController(WebDbContext webDbContext, TaskRepository taskRepository, AuthService authService)
        {
            _taskRepository = taskRepository;
            _authService = authService;
        }

        public IActionResult Index()
        {
            var a  = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "Привет";

            var dbTasks = _taskRepository.GetTasks();
            var viewModels = dbTasks.Select(dbTask => 
            {
                return new TaskViewModel
                {
                    Id = dbTask.Id,
                    Name = dbTask.Name,
                    Description = dbTask.Description,
                    Priority = dbTask.Priority,
                    Owner = dbTask.Owner?.Login
                };
                }).ToList();
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            var viewModel = new AddTaskViewModel
            {
                PriorityOptions = new List<int> { 1, 2, 3 }
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddTask(AddTaskViewModel taskViewModel)
        {
            if (!ModelState.IsValid)
            {
                taskViewModel.PriorityOptions = new List<int> { 1, 2, 3 };
                return View(taskViewModel);
            }

            var task = new TaskInfo
            {
                Name = taskViewModel.Name,
                Description = taskViewModel.Description,
                Priority = taskViewModel.Priority,
                Owner = _authService.GetCurrentUser()
            };

            _taskRepository.AddTask(task);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateTask(int id)
        {
            var dbTask = _taskRepository.GetTaskById(id);
            var viewModel = new AddTaskViewModel
            {
                Id = dbTask.Id,
                Name = dbTask.Name,
                Description = dbTask.Description,
                Priority = dbTask.Priority,
                PriorityOptions = new List<int> { 1, 2, 3 }

            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateTask(AddTaskViewModel taskViewModel)
        {
            if (!ModelState.IsValid)
            {
                taskViewModel.PriorityOptions = new List<int> { 1, 2, 3 };
                return View(taskViewModel);
            }

            _taskRepository.UpdateTask(taskViewModel);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(int id)
        {
            _taskRepository.DeleteTask(id);
            return RedirectToAction("Index");
        }

    }
}
