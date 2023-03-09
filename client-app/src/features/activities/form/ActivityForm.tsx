import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { Button, Form, Segment } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Activity } from "../../../app/models/activity";
import { useStore } from "../../../app/stores/store";
import { v4 as uuid } from "uuid";

export default observer(function ActivityForm() {
  const { activityStore } = useStore();
  const { createActivity, updateActivity, loading, loadActivity, loadingInitial } = activityStore;
  const { id } = useParams();
  const navigate = useNavigate();

  const [activity, setActivity] = useState<Activity>({
    id: '',
    title: '',
    category: '',
    description: '',
    date: '',
    city: '',
    venue: '',
  });

  useEffect(() => {
    if (id) loadActivity(id).then(activity => setActivity(activity!));
  }, [id, loadActivity]);

  function handleSubmit() {
    if (!activity.id) {
      activity.id = uuid();
      createActivity(activity).then(() => { navigate(`/activities/${activity.id}`) });
    }
    else {
      updateActivity(activity).then(() => { navigate(`/activities/${activity.id}`) });
    }
  }

  function handleInputChange(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
    const { name, value } = event.target;
    setActivity({ ...activity, [name]: value });
  }

  if (loadingInitial) return <LoadingComponent content="Loading app..." />

  return (
    <Segment clearing>
      <Form>
        <Form.Input placeholder='Title' name="title" value={activity.title} onChange={handleInputChange} />
        <Form.TextArea placeholder='Description' name="description" value={activity.description} onChange={handleInputChange} />
        <Form.Input placeholder='Category' name="category" value={activity.category} onChange={handleInputChange} />
        <Form.Input type='date' placeholder='Date' name="date" value={activity.date} onChange={handleInputChange} />
        <Form.Input placeholder='City' name="city" value={activity.city} onChange={handleInputChange} />
        <Form.Input placeholder='Venue' name="venue" value={activity.venue} onChange={handleInputChange} />
        <Button loading={loading} onClick={handleSubmit} floated='right' positive type='submit' content='Submit' />
        <Button as={Link} to='/activities' floated='right' type='button' content='Cancel' />
      </Form>
    </Segment>
  )
});