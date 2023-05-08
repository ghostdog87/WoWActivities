import { observer } from "mobx-react-lite";
import Calendar from "react-calendar";
import { Header, Menu } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

export default observer(function ActivityFilters() {
  const { activityStore: { filter, setFilter } } = useStore();

  return (
    <>
      <Menu vertical size='large' style={{ width: '100%', marginTop: 25 }}>
        <Header icon='filter' attached color='teal' content='Filters' />
        <Menu.Item content='All Activities' active={filter.has('all')} onClick={() => setFilter('all', 'true')} />
        <Menu.Item content="I'm going" active={filter.has('isGoing')} onClick={() => setFilter('isGoing', 'true')} />
        <Menu.Item content="I'm hosting" active={filter.has('isHost')} onClick={() => setFilter('isHost', 'true')} />
      </Menu>
      <Header />
      <Calendar onChange={(date: any) => setFilter('startDate', date as Date)} value={filter.get('startDate') || new Date()} />
    </>
  );
})