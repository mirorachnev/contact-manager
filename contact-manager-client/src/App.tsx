import React from 'react';
import './App.css';

import { ContactList } from './components/ContactList';

function App() {
  return (
    <div className="container">
      <h3 className="m-3 d-flex justify-content-center">
        Contact Manager
      </h3>

      <br />

      <ContactList />
    </div>
  );
}

export default App;
