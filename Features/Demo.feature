Feature: User profile management simple

  @simple
  Scenario: Registration validation
    Given Prepare header and payload for registration
    When Call api registration
    Then Validate response for registration
