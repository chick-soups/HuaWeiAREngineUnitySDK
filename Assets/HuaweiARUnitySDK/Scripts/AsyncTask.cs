//-----------------------------------------------------------------------
// <copyright file="AsyncTask.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------
namespace HuaweiARUnitySDK
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using HuaweiARInternal;

    /**
     * \if english
     * @brief A class used for monitoring the status of an asynchronous task.
     * @tparam T The resultant type of the task.
     * \else
     * @brief 管理异步任务的类。
     * @tparam T 任务的结果类型。
     * \endif
     */
    public class AsyncTask<T>
    {
        private List<Action<T>> actionListAfterTaskCompletion;

        /**
         * \if english
         * Gets the result of a completed task.
         * \else
         * 获取已完成的任务结果。
         * \endif
         */
        public T TaskResult { get; private set; }

        /**
         * \if english
         * Gets a value indicating whether the task is complete.
         * \else
         * 获取当前任务的完成状态。
         * \endif
         */
        public bool IsTaskCompleted { get; private set; }

        /**
         * \if english
         * @brief Constructor for AsyncTask that creates a completed task.
         * @param result The result of the completed task.
         * \else
         * @brief 构造一个已经完成的异步任务。
         * @param result 异步任务的结果。
         * \endif
         */
        public AsyncTask(T result)
        {
            TaskResult = result;
            IsTaskCompleted = true;
        }

        /**
         * \if english
         * @brief Constructor for AsyncTask.
         * @param[out] asyncPerformActionAfterTaskCompleted A callback that, when invoked, changes the status of 
         * the task to complete and sets the result based on the argument supplied.
         * \else
         * @brief AsyncTask构造函数。
         * @param[out] asyncPerformActionAfterTaskCompleted 当异步任务完成后执行的回调方法。
         * \endif
         */
        public AsyncTask(out Action<T> asyncPerformActionAfterTaskCompleted)
        {
            IsTaskCompleted = false;
            asyncPerformActionAfterTaskCompleted = delegate (T result)
            {
                TaskResult = result;
                IsTaskCompleted = true;
                if (actionListAfterTaskCompletion != null)
                {
                    AsyncTask.AddTask(() =>
                    {
                        for (int i = 0; i < actionListAfterTaskCompletion.Count; i++)
                        {
                            actionListAfterTaskCompletion[i](result);
                        }
                    });
                }
            };
        }

        /**
         * \if english
         * @brief Returns a yield instruction that monitors this task for completion within a coroutine.
         * @return A yield instruction that monitors this task for completion.
         * \else
         * @brief 返回一个yield指令，用于监控异步任务在协程中的执行状态。
         * @return 一个yield指令，用于监控异步任务在协程中的执行状态。
         * \endif
         */
        public CustomYieldInstruction GetWaitForCompletionYieldInstruction()
        {
            return new WaitForAsynTaskCompletionYieldInstruction<T>(this);
        }

        /**
         * \if english 
         * @brief Performs an action (callback) in the AsyncTask.Update() call after task completion. 
         * @param actionAfterTask The action to invoke when task is complete.  The result of the task will 
         * be passed as an argument to the action.
         * @return The invoking asynchronous task.
         * \else
         * @brief 指定异步任务完成后执行的Action，该Action在AsyncTask.Update()中执行。
         * @param actionAfterTask 异步任务完成后执行的Action，异步任务的结果会被当做action的入参。
         * @return 当前的异步任务。
         * \endif
         */
        public AsyncTask<T> ThenAction(Action<T> actionAfterTask)
        {
            if (IsTaskCompleted)
            {
                actionAfterTask(TaskResult);
                return this;
            }

            if (actionListAfterTaskCompletion == null)
            {
                actionListAfterTaskCompletion = new List<Action<T>>();
            }

            actionListAfterTaskCompletion.Add(actionAfterTask);
            return this;
        }
    }

    /**
     * \if english
     * @brief Helper methods for dealing with asynchronous tasks.
     * \else
     * @brief 帮助处理异步任务的类。
     * \endif
     */
    public class AsyncTask
    {
        private static Queue<Action> actionQueue = new Queue<Action>();

        private static object for_lock = new object();

        /**
         * \if english
         * @brief Queues an action to be performed in Update(). This method can be called by any thread.
         * @param action The action to perform.
         * \else
         * @brief 添加一个在Update()函数中执行的任务。该方法可以在任意线程中调用。
         * @param action 要执行的action.
         * \endif
         */
        public static void AddTask(Action action)
        {
            lock (for_lock)
            {
                actionQueue.Enqueue(action);
            }
        }

        /**
         * \if english
         * @brief An Update handler should called each frame in unity 
         * <a href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html">Update()</a>.
         * \else
         * @brief 执行所有添加的任务，该方法应该在unity的
         * <a href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html">Update()</a>
         * 函数中使用。
         * \endif
         */
        public static void Update()
        {
            lock (for_lock)
            {
                while (actionQueue.Count > 0)
                {
                    actionQueue.Dequeue().Invoke();
                }
            }
        }
    }
}
