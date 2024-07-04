import React from 'react';
import { Provider } from 'react-redux';
import store from './store';
import CategoryForm from './components/CategoryForm';
import TaskForm from './components/TaskForm';
import TaskList from './components/TaskList';

const App = () => {
    return (
        <Provider store={store}>
            <div className="container">
                <CategoryForm />
                <TaskForm />
                <TaskList />
            </div>
        </Provider>
    );
};

export default App;
