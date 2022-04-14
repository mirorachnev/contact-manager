import React, { Component } from 'react';
import { Contact } from '../models/contact';
import contactsService from '../services/contactsService';

export class Contacts extends Component {
  state: { contacts: Contact[] };

  constructor(props: {} | Readonly<{}>) {
    super(props);
    this.state = {contacts:[]};
  }

  loadContacts = async () => {
    
    const contacts = await contactsService.getContacts();
    this.setState({ contacts: contacts });

    console.log(this.state);
  }

  componentDidMount() {
    this.loadContacts();
  }

  render() {
    return (
      <div className="mt-5 d-flex justify-content-left">
        This is contacts page.
      </div>
    )
  }
}