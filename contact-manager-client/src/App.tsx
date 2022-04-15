import React from 'react';
import './App.css';

import { Home } from './components/Home';
import { ContactList } from './components/ContactList';
import { Navigation } from './components/Navigation';

import { BrowserRouter, Route, Routes } from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
      <div className="container">
        <h3 className="m-3 d-flex justify-content-center">
          React JS Tutorial
        </h3>

        <Navigation />

        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/contacts' element={<ContactList />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
