import React, { useState } from 'react';
import { useDispatch } from 'react-redux';

const CategoryForm = () => {
    const [name, setName] = useState('');
    const dispatch = useDispatch();

    const handleSubmit = (e) => {
        e.preventDefault();
        dispatch({ type: 'ADD_CATEGORY', payload: { id: Date.now(), name } });
        setName('');
    };

    return (
        <form onSubmit={handleSubmit} className="form mt-4">
            <div className="row">
                <h5>Create Category</h5>
                <div className="form-group col-4">
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </div>
                <div className="form-group col-2 d-flex align-items-end">
                    <button type="submit" className="btn btn-outline-primary">Create</button>
                </div>
            </div>
        </form>
    );
};

export default CategoryForm;
