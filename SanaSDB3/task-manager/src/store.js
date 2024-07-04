import { createStore, combineReducers } from 'redux';

const initialState = {
  categories: [],
  tasks: []
};

function categoryReducer(state = initialState.categories, action) {
  switch (action.type) {
    case 'ADD_CATEGORY':
      return [...state, action.payload];
    default:
      return state;
  }
}

function taskReducer(state = initialState.tasks, action) {
  switch (action.type) {
    case 'ADD_TASK':
      return [...state, action.payload];
    case 'TOGGLE_TASK':
      return state.map(task =>
        task.id === action.payload
          ? { ...task, completed: !task.completed }
          : task
      );
    case 'DELETE_TASK':
      return state.filter(task => task.id !== action.payload);
    default:
      return state;
  }
}

const rootReducer = combineReducers({
  categories: categoryReducer,
  tasks: taskReducer
});

const store = createStore(rootReducer);

export default store;
