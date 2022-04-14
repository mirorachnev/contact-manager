import React, { Component } from 'react';
import { Button, ButtonToolbar, Table } from 'react-bootstrap';
import { Contact } from '../models/contact';
import { AddEditContact } from './AddEditContact';
import contactsService from '../services/contactsService';

export class Contacts extends Component {
  state: { contacts: Contact[], addEditModalShow: boolean, contact: Contact };

  constructor(props: {} | Readonly<{}>) {
    super(props);
    this.state = {
      contacts: [],
      addEditModalShow: false,
      contact: {
        firstName: '',
        lastName: '',
        address: '',
        phoneNumber: '',
        dateOfBirth: '',
        email: '',
        id: '',
        iban: ''}
    };
  }

  getContacts = async () => {
    
    const contacts = await contactsService.getContacts();
    this.setState({ contacts: contacts });
  }

  deleteContact = async (id: string) => {
    if (window.confirm('Are you sure?')) {
      await contactsService.deleteContact(id);

      this.getContacts();
    }    
  }

  componentDidMount() {
    this.getContacts();
  }

  render() {

    return (
      <>
        <Table className="mt-4" striped bordered hover size="sm">
          <thead>
            <tr>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Email</th>
              <th>Phone Number</th>
              <th>Date of Birth</th>
              <th>Address</th>
              <th>IBAN</th>
            </tr>
          </thead>
          <tbody>
            {this.state.contacts.map(contact =>
              <tr key={contact.id}>
                <td>{contact.firstName}</td>
                <td>{contact.lastName}</td>
                <td>{contact.email}</td>
                <td>{contact.phoneNumber}</td>
                <td>{contact.dateOfBirth}</td>
                <td>{contact.address}</td>
                <td>{contact.iban}</td>
                <td>
                  <ButtonToolbar>
                    <Button className="mr-2" variant="info"
                      onClick={() => this.setState({contact: contact, addEditModalShow: true})}>
                      
                      Edit
                    </Button>

                    <Button className="mr-2" variant="danger"
                      onClick={() => this.deleteContact(contact.id)}>
                      Delete
                    </Button>

                    
                  </ButtonToolbar>

                </td>

              </tr>)}
          </tbody>

        </Table>

        <ButtonToolbar>
          <Button variant='primary'
            onClick={() => { this.setState({ addEditModalShow: true }); }}>
            Add Contact</Button>

          <AddEditContact
            show={this.state.addEditModalShow}
            isedit={false} contact={(
              {
                firstName: '',
                lastName: '',
                address: '',
                phoneNumber: '',
                dateOfBirth: '',
                email: '',
                id: '',
                iban: ''
              })}
            onHide={() => this.setState({ addEditModalShow: false })} />
        </ButtonToolbar>

        
      </>
    )
  }
}