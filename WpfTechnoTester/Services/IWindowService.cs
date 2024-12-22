﻿using Domain.Models;

namespace WpfTechnoTester.Services
{
    public interface IWindowService
    {
        void ShowTodoItemEditViewDialog();
        void ShowTodoItemEditViewDialog(TodoItem todoItem);
        void ShowNewUserSignupDialog();
        bool ShowUserLoginDialog();
    }
}
