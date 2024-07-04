import React from 'react';
import { useDispatch, useSelector } from 'react-redux';

const formatDate = (dateString) => {
    const options = { day: 'numeric', month: 'long', hour: '2-digit', minute: '2-digit' };
    const date = new Date(dateString);
    return date.toLocaleDateString('uk-UA', options);
};

const TaskList = () => {
    const tasks = useSelector(state => state.tasks);
    const categories = useSelector(state => state.categories);
    const dispatch = useDispatch();

    const toggleTask = (id) => {
        dispatch({ type: 'TOGGLE_TASK', payload: id });
    };

    const deleteTask = (id) => {
        dispatch({ type: 'DELETE_TASK', payload: id });
    };

    return (
        <table className="table table-hover task-table mt-4 border-top">
            <tbody>
                {tasks.filter(task => !task.completed).map(task => (
                    <tr key={task.id}>
                        <td width="30px">
                            <input
                                style={{ height: '20px', width: '20px' }}
                                className="form-check-input"
                                type="checkbox"
                                checked={task.completed}
                                onChange={() => toggleTask(task.id)}
                            />
                        </td>
                        <td className="text-start" width="600px">
                            {task.name}
                        </td>
                        <td width="250px">{formatDate(task.dueDate)}</td>
                        <td width="200px">
                            {task.categoryId && (
                                <p className="primary-bg">
                                    {categories.find(category => category.id == task.categoryId)?.name}
                                </p>
                            )}
                        </td>
                        <td className="text-end">
                            <button onClick={() => deleteTask(task.id)} className="me-4" style={{ color: 'red', border: 'none', background: 'none' }}>
                                <i className="fa fa-close" style={{ fontSize: '25px' }}></i>
                            </button>
                        </td>
                    </tr>
                ))}
            </tbody>
            <tbody className="completed-table">
                {tasks.filter(task => task.completed).map(task => (
                    <tr key={task.id} className="completed-task">
                        <td width="30px">
                            <input
                                style={{ height: '20px', width: '20px' }}
                                className="form-check-input"
                                type="checkbox"
                                checked={task.completed}
                                onChange={() => toggleTask(task.id)}
                            />
                        </td>
                        <td className="text-start" width="600px">
                            {task.name}
                        </td>
                        <td width="250px">{formatDate(task.dueDate)}</td>
                        <td width="200px">
                            {task.categoryId && (
                                <p className="secondary-bg">
                                    {categories.find(category => category.id == task.categoryId)?.name}
                                </p>
                            )}
                        </td>
                        <td className="text-end">
                            <button onClick={() => deleteTask(task.id)} className="me-4" style={{ color: 'gray', border: 'none', background: 'none' }}>
                                <i className="fa fa-close" style={{ fontSize: '25px' }}></i>
                            </button>
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default TaskList;
