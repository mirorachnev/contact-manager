import React, { useEffect, useState } from 'react';
import { Button, ButtonToolbar, Table } from 'react-bootstrap';
import { Contact } from '../models/contact';
import { AddContact } from './AddContact';
import { EditContact } from './EditContact';
import contactsService from '../services/contactsService';

function ContactList() {

  const [contacts, setContacts] = useState<Contact[]>([]);
  const [addModalShow, setAddModalShow] = useState(false);
  const [editModalShow, setEditModalShow] = useState(false);
  const [contact, setContact] = useState<Contact>({
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    address: '',
    dateOfBirth: '',
    iban: '',
    id: ''
  });  

  const getContacts = async () => {
    const contacts = await contactsService.getContacts();    
    setContacts(contacts);
  }

  useEffect(() => {
    getContacts();
  }, []);    

  const deleteContact = async (id: string) => {
    if (window.confirm('Are you sure?')) {
      await contactsService.deleteContact(id);
      getContacts();
    }    
  }

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
          <th></th>
        </tr>
      </thead>
      <tbody>
        {contacts.map(contact =>
          <tr key={contact.id}>
            <td>{contact.firstName}</td>
            <td>{contact.lastName}</td>
            <td>{contact.email}</td>
            <td>{contact.phoneNumber}</td>
            <td>{new Date(contact.dateOfBirth).toLocaleDateString()}</td>
            <td>{contact.address}</td>
            <td>{contact.iban}</td>
            <td>
              <ButtonToolbar>
                <Button className="mr-2" variant="info"
                  onClick={() => { setEditModalShow(true); setContact(contact); }}>
                  Edit
                </Button>

                <Button className="mr-2" variant="danger"
                  onClick={() => deleteContact(contact.id)}>
                  Delete
                </Button>
              </ButtonToolbar>
            </td>
          </tr>)}
      </tbody>
    </Table>

    <ButtonToolbar>
      <Button variant='primary'
        onClick={() => setAddModalShow(true)}>
        Add Contact</Button>
    </ButtonToolbar>

    <AddContact
      show={addModalShow}
      onHide={() => setAddModalShow(false)}
      reloadData={() => getContacts()}
    />

    <EditContact
      show={editModalShow}
      onHide={() => setEditModalShow(false)}
      reloadData={() => getContacts()}
      contact={contact}
    />
  </>
  );    
}

export default ContactList