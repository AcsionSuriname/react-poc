import React from 'react';
import { Provider } from 'react-redux';
import { createStore, applyMiddleware } from 'redux'
import thunkMiddleware from 'redux-thunk'
import rootReducer from './reducers';

// Create a store with the reduce output (rootReducer)
// Add thunk middleware to enable dispatch method calls (only partially understand this)
function configureStore(preloadedState) {
    return createStore(
        rootReducer,
        preloadedState,
        applyMiddleware(thunkMiddleware)
    )
}

const store = configureStore();

export default ({ element }) => (
    <Provider store={store}>{element}</Provider>
);