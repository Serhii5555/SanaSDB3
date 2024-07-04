import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

const TaskForm = () => {
    const [name, setName] = useState('');
    const [dueDate, setDueDate] = useState('');
    const [categoryId, setCategoryId] = useState('');
    const dispatch = useDispatch();
    const categories = useSelector(state => state.categories);

    const handleSubmit = (e) => {
        e.preventDefault();
        dispatch({
            type: 'ADD_TASK',
            payload: { id: Date.now(), name, dueDate, categoryId, completed: false }
        });
        setName('');
        setDueDate('');
        setCategoryId('');
    };

    return (
        <form onSubmit={handleSubmit} className="form mt-4">
            <div className="row">
                <h5>Create Task</h5>
                <div className="form-group col-4">
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </div>
                <div className="form-group col-2">
                    <input
                        type="datetime-local"
                        className="form-control"
                        value={dueDate}
                        onChange={(e) => setDueDate(e.target.value)}
                    />
                </div>
                <div className="form-group col-2">
                    <select
                        className="form-control"
                        value={categoryId}
                        onChange={(e) => setCategoryId(e.target.value)}
                    >
                        <option value="">No Category</option>
                        {categories.map(category => (
                            <option key={category.id} value={category.id}>{category.name}</option>
                        ))}
                    </select>
                </div>
                <div className="form-group col-2 d-flex align-items-end">
                    <button type="submit" className="btn btn-outline-primary">Create</button>
                </div>
            </div>
        </form>
    );
};

export default TaskForm;
